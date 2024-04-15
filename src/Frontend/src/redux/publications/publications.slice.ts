import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import { publicationInitialState } from "./publication.initial-state";
import { Publication } from "../../types/publications.types";
import { IFileUploadData } from "../../types/search.types";

export const publicationsSlice = createSlice({
    name: 'publications',
    initialState: publicationInitialState,
    reducers: {
        fetchPublications(store){
            store.isLoading = true
        },
        fetchPublicationsSuccess(store, action: PayloadAction<{publications: Publication[]}>) {
            store.publications = action.payload.publications;
            store.isLoading = false;
        },
        fetchPublicationsError(store, action: PayloadAction<string>){
            store.isLoading = false;
            store.error = action.payload;
        },
        setActivePublication(store, action: PayloadAction<{publication: Publication}>){
            store.activePublication = action.payload.publication;
        },
        unsetActivePublication(store){
            store.activePublication = null
        },
        uploadFile(store, action: PayloadAction<IFileUploadData>){
            store.isLoading = true;
        },
        uploadFileSuccess(store){
            store.isLoading = false;
        },
        uploadFileError(store, action: PayloadAction<string>){
            store.isLoading = false;
            store.error = action.payload;
        }

    }
})


export const {
    fetchPublications,
    fetchPublicationsSuccess,
    fetchPublicationsError,
    setActivePublication,
    unsetActivePublication,
    uploadFile,
    uploadFileSuccess,
    uploadFileError
} = publicationsSlice.actions;

export const publicationsReducer = publicationsSlice.reducer
