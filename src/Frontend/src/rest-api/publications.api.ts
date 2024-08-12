import axios from 'axios';
import { RestApiAdapter } from '../helpers/rest-api.adapter';
import { Publication } from '../types/publications.types';
import { IFileUploadData, ISearchType, ImportFileType } from '../types/search.types';

const restApiAdapter = new RestApiAdapter()


// Get publications
export const getPublications = async (): Promise<Publication[]> => { 
    const response = await restApiAdapter.get(`Publications`);
    
    if (response.status !== 200) {
        throw new Error('Error fetching publications');
    }

    return response.data.payload
}

export const postPublications = async (publications: Partial<Publication>[]) => {
    return (await restApiAdapter.post(`Publications`, publications)).data.payload;
}

export const putPublication = async(publication:Publication) => {
    return (await restApiAdapter.put(`Publications/${publication.id}`, publication)).data.payload;
}

export const patchPublication = async(publication: Publication) => {
    return (await restApiAdapter.patch(`Publications/${publication.id}`, publication)).data.payload;
}

export const deletePublication = async(id: string) => { 
    return (await restApiAdapter.delete(`Publications/${id}`)).data.payload
}

export const searchPublications = async(searchObj: ISearchType): Promise<Publication[]> => {
    const response = await restApiAdapter.post(`Publications/Search`, searchObj)

    if (response.status !== 200 ) {
        throw new Error('Error fetching filtered publications');
    }


    return response.data.payload as Publication[];
}

export const importPublications = async(file: ImportFileType) => {
    return (await restApiAdapter.post(`Publications/ImportAsStream`, file)).data.payload;
}

export const uploadFileRequest = async (data: IFileUploadData) => {
    const formData = new FormData();
    formData.append('file', data.file);

    try {
        const response = await restApiAdapter.post(
            `Publications/ImportAsStream?formatType=${data.formatType}&source=${data.source}&discardInvalidRecords=${data.discardInvalidRecords}`,
            formData
        );
        return response
    } catch (error) {
        throw error;
    }
};


export const exportPublications = async(file: ImportFileType) => { 
    return (await restApiAdapter.post(`Publications/Export`, file)).data.payload
}

export const exportPublicationsAsStream = async(fileFormat: number, pubs: Publication[]) => {
    try {
        const response = await axios.post(
            `Publications/ExportAsStream?formatType=${fileFormat}`,
            pubs,
            { responseType: 'blob' }
        )

        // Determine file type and extension based on the format type
        let fileExtension = fileFormat === 1 ? 'csv' : 'bib';
        let mimeType = fileFormat === 1 ? 'text/csv' : 'text/x-bibtex';

        // Create a Blob from the response data
        const blob = new Blob([response.data], { type: mimeType });
        
        // Create a link element
        const link = document.createElement('a');
        
        // Create a URL for the Blob and set it as the href attribute
        link.href = window.URL.createObjectURL(blob);
        
        // Set the download attribute with a filename
        link.download = `publications.${fileExtension}`;
        
        // Append the link to the body (required for Firefox)
        document.body.appendChild(link);
        
        // Trigger the download by simulating a click
        link.click();
        
        // Clean up by removing the link
        document.body.removeChild(link);

        return response

    } catch (error) {
        throw error
    }

}


