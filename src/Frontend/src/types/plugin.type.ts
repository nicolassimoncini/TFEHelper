interface IPluginParameter {
    singleValued: any;
    collectionValued: {
        name: string,
        value: {
            name: string,
            value: string
        }[]
    }[]
}

export interface IPlugin {
    id: number,
    type: number,
    name: string,
    version: string,
    description: string,
    parameters: IPluginParameter | null;
}