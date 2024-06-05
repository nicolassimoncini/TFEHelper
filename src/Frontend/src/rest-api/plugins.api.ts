import { RestApiAdapter } from "../helpers/rest-api.adapter";
import { IPlugin } from "../types/plugin.type";
import { PluginCollectorQuery } from "../types/search.types";

const restApiAdapter = new RestApiAdapter()

export const getPlugins = async(): Promise<IPlugin[]> => { 
    const res = await restApiAdapter.get(`Plugins`);

    if(res.status !==200){
        throw new Error('Error fetching plugins');
    }

    return res.data.payload as IPlugin[]

}

export const searchInPlugins = async(id: string, searchObj: PluginCollectorQuery) => { 
    return await restApiAdapter.post(`/Plugins/Collectors/${id}`, searchObj);
}

