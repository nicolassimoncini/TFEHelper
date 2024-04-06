export interface ConfigurationItem {
    name: string;
    value: number;
}

export interface ConfigurationType {
    name: string;
    items: ConfigurationItem[];
}


interface BibTexPublication extends ConfigurationType {}

interface FileFormatType extends ConfigurationType {}

interface SearchSourceType extends ConfigurationType{}

export interface IConfigurations { 
    BibTexConfig: BibTexPublication;
    FileFormatTypeConfig: FileFormatType;
    SearchSourceTypeConfig: SearchSourceType;
}

export interface IConfigurationState extends IConfigurations{
    isLoading: boolean;
    isError: boolean;
    error: string | null;
}