import { RestApiAdapter } from "../helpers/rest-api.adapter";
import { IPlugin } from "../types/plugin.type";
import { Publication } from "../types/publications.types";
import { PluginCollectorQuery } from "../types/search.types";

const restApiAdapter = new RestApiAdapter()

export const getPlugins = async(): Promise<IPlugin[]> => { 
    const res = await restApiAdapter.get(`Plugins`);

    if(res.status !==200){
        throw new Error('Error fetching plugins');
    }

    return res.data.payload as IPlugin[]

}

export const searchInPlugins = async(id: string, searchObj: PluginCollectorQuery): Promise<Publication[]> => { 
    try {
        const res =  await restApiAdapter.post(`Plugins/Collectors/${id}/Run`, searchObj);
        
        return res.data.payload as Publication[]
    } catch (error) {
        console.error()
        throw new Error('Error while fetching publications');
    }
}

