import { RestApiAdapter } from "../helpers/rest-api.adapter";

const restApiAdapter = new RestApiAdapter()

export const getConfigurations = async() => { 
    return await restApiAdapter.get(`/Configurations/Enumerators`);
}