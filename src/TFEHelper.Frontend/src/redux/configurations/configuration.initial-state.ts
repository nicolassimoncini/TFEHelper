import { IConfigurationState } from '../../types/configurations.types';

export const  configurationInitialState: IConfigurationState = {
    isLoading: true,
    isError: false,
    error: null,
    BibTexConfig: {
        name: 'BibTeXPublicationType',
        items: []
    },
    FileFormatTypeConfig: {
        name: 'FileFormatType',
        items: []
    },
    SearchSourceTypeConfig: {
        name: 'SearchSourceType',
        items: []
    }
}