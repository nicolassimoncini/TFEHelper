import { PublicationState } from "../../types/publications.types";

export const publicationInitialState: PublicationState = {
    activePublication: null,
    publications: [],
    isLoading: true,
    error: null
}