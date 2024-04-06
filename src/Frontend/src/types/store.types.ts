import { Publication } from "./publications.types";

export interface StatusState {
    isLoading: boolean;
    error: string | null;
}

export interface Store{
    publications: Publication[];
    status: StatusState;
}