import { IConfigurationState } from "./configurations.types";
import { PublicationState } from "./publications.types";

export interface StatusState {
    isLoading: boolean;
    error: string | null;
}

export interface Store{
    publication: PublicationState;
    configuration: IConfigurationState
    status: StatusState;
}