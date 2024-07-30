import { ConfigurationItem } from "./configurations.types";

export interface Publication {
    id: string | number;
    type: ConfigurationItem | number;
    key: string | number;
    source: ConfigurationItem | number;
    url: string| null;
    title: string| null;
    authors: string| null;
    keywords: string| null
    doi: string| null;
    year: number| null;
    isbn: string| null;
    issn: string| null;
    abstract: string| null;
    pages:string| null;
}

export interface PublicationState{
    isLoading: boolean,
    error: string | null,
    activePublication: Publication | null;
    publications: Publication[]
}