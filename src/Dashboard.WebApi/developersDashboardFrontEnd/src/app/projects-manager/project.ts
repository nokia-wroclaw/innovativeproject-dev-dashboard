export interface Pipeline {
    id : number;
    ref : string;
    status : string;
    commitTitle : string,
    commiterName : string;
    commiterEmail : string;

    stages : Stage[];
}

export interface Stage {
    stageName : string;
    jobs : Job[];
}

export interface Job {
    name : string;
    status : string;
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