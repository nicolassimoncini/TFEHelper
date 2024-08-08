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
        console.error(response.data.errorMessage)
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

export const exportPublicationsAsStream = async(fileFormat: number) => {
    try {
        const response = await restApiAdapter.post(
            `Publications/ExportAsStream`,{
                formatType: fileFormat
            }
        )

        return response

    } catch (error) {
        throw error
    }

}


