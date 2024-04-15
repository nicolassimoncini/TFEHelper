import axios, { AxiosResponse } from 'axios'

const baseURL: string = process.env.REACT_APP_API_URL as string || 'http://localhost:4000/api';


interface restApiResponse extends AxiosResponse {
    data: {
        status: number,
        isSuccesful: boolean,
        errorMessage: string[],
        payload: any,
        totalPages: number
    }
}

interface restApiAdapterInterface {
    get(url:string): Promise<restApiResponse>;
    post(url: string, body: any):  Promise<restApiResponse>
    put(url:string, body: any):  Promise<restApiResponse>;
    delete(url: string, body: any):  Promise<restApiResponse>;
    patch(url:string, body: any):  Promise<restApiResponse>;
}


export class RestApiAdapter implements restApiAdapterInterface {
    
    async get(url: string): Promise<restApiResponse> {
        return await axios.get(`${baseURL}/${url}`) as restApiResponse;
    }
    async post(url: string, data: any): Promise<restApiResponse> {
        if (data instanceof FormData) {
            return await axios.post(`${baseURL}/${url}`, data, {
                headers: {
                    'Content-Type': 'multipart/form-data',
                },
            }) as restApiResponse;
        } else {
            return await axios.post(`${baseURL}/${url}`, data) as restApiResponse;
        }
    }

    async put(url: string, data: any): Promise<restApiResponse> {
        return await axios.put(`${baseURL}/${url}`, data) as restApiResponse;
    }
    async delete(url: string): Promise<restApiResponse> {
        return await axios.delete(`${baseURL}/${url}`) as restApiResponse;
    }
    async patch(url: string, data: any): Promise<restApiResponse> {
        return await axios.patch(`${baseURL}/${url}`, data) as restApiResponse;
    }
}