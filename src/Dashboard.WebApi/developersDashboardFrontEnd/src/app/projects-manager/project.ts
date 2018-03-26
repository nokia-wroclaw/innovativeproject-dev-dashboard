export interface Pipeline {
    id : number;
    ref : string;
    status : string;
    stages : Stage[];
}

export interface Stage {
    stageName : string;
    jobs : Job[];
}

export enum JobStatus {
    created, manual, success, prepare, canceled, failed, running
}

export interface Job {
    name : string;
    status : JobStatus;
}

export class Project {
    id : number;
    apiHostUrl : string;
    apiProjectId : string;
    apiAuthenticationToken : string;
    dataProviderName : string;
    pipelines : Pipeline[];
    constructor(apiHostUrl : string, apiProjectId : string, apiAuthenticationToken : string, dataProviderName : string, pipelines : Pipeline[]) {}
    
}