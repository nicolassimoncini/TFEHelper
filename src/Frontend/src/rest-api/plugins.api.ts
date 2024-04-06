import { RestApiAdapter } from "../helpers/rest-api.adapter";
import { PluginCollectorQuery } from "../types/search.types";

const restApiAdapter = new RestApiAdapter()

export const getPlugins = async() => { 
    return await restApiAdapter.get(`/Plugins`);
}

export const searchInPlugins = async(id: string, searchObj: PluginCollectorQuery) => { 
    return await restApiAdapter.post(`/Plugins/Collectors/${id}`, searchObj);
}

