import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { configurationInitialState } from "./configuration.initial-state";
import { IConfigurations } from '../../types/configurations.types';

export const configurationSlice = createSlice({
    name: 'configuration',
    initialState: configurationInitialState,
    reducers: {
        fetchConfiguration(store){
            store.isLoading = true
        },
        fetchConfigurationSuccess(store, action: PayloadAction<{configurations: IConfigurations}>){
            store.BibTexConfig = action.payload.configurations.BibTexConfig;
            store.FileFormatTypeConfig = action.payload.configurations.FileFormatTypeConfig;
            store.SearchSourceTypeConfig = action.payload.configurations.SearchSourceTypeConfig;
            store.isLoading = false;
        },
        fetchConfigurationError(store, action: PayloadAction<string>){
            store.isLoading = false;
            store.isError = true;
            store.error = action.payload
        },
    }

})

export const {
    fetchConfiguration,
    fetchConfigurationSuccess,
    fetchConfigurationError
} = configurationSlice.actions

export const configurationReducer = configurationSlice.reducer