export interface Pipeline {
    id : number;
    sha : string;
    ref : string;
    status : string;
}

export interface Project {
    id : number;
    apiHostUrl : string;
    apiProjectId : string;
    apiAuthenticationToken : string;
    dataProviderName : string;
    pipelines : Pipeline[];
}