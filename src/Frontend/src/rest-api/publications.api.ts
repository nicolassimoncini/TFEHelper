import { RestApiAdapter } from '../helpers/rest-api.adapter';
import { Publication } from '../types/publications.types';
import { ISearchType, ImportFileType } from '../types/search.types';

const restApiAdapter = new RestApiAdapter()


// Get publications
export const getPublications = async (): Promise<Publication[]> => { 
    return (await restApiAdapter.get(`Publications`)).data.payload;
}

export const postPublications = async (publications: Publication[]) => {
    return (await restApiAdapter.post(`Publications`, publications)).data.payload;
}

export const putPublication = async(publication:Publication) => {
    return (await restApiAdapter.put(`Publications/${publication.id}`, publication)).data.payload;
}

export const patchPublication = async(publication: Publication) => {
    return (await restApiAdapter.patch(`Publications/${publication.id}`, publication)).data.payload;
}

export const searchPublications = async(searchObj: ISearchType) => {
    return (await restApiAdapter.post(`Publications/Search`, searchObj)).data.payload;
}

export const importPublications = async(file: ImportFileType) => {
    return (await restApiAdapter.post(`Publications/Import`, file)).data.payload;
}

export const exportPublications = async(file: ImportFileType) => { 
    return (await restApiAdapter.post(`Publications/Export`, file)).data.payload
}


