export interface Pipeline {
    id : number;
    sha : string;
    ref : string;
    status : string;
}

export class Project {
    id : number;
    constructor(apiHostUrl : string, apiProjectId : string, apiAuthenticationToken : string, dataProviderName : string, pipelines : Pipeline[]) {}

}