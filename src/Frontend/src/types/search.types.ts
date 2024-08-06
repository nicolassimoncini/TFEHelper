export interface INarrowings {
  id: number;
  searchIn: string;
  firstInput: string;
  secondInput: string;
  distance: number;
}

export interface ISearchType{
    query: string;
    parameters: {
        name: string;
        value: string | number;
    }[];
    narrowings: {
        fieldName: string;
        firstSentence: string;
        secondSentence: string;
        minimumDistance: number;
    }[]
}

export interface IFileUploadData{
    formatType: number;
    source: number;
    discardInvalidRecords: boolean
    file: File
}

export interface ImportFileType{
    filePath: string;
    formatType: number;
    source: number;
    discardInvalidRecords: boolean;
}

export interface ExportFileType{
    filePath: string;
    formatType: number;
}

export interface PluginCollectorQuery{
    query: string;
    searchIn: string;
    subject: string;
    dateFrom: string;
    dateTo: string;
    returnQuantityLimit: number;
}