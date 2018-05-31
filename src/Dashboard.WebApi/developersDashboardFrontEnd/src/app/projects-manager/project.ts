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
    stageStatus : string;
}

export class SupportedProviders {
    data : string[];
    constructor(data : string[]) {}
}

export class Project {

    id : number;
    projectTitle : string;
    apiHostUrl : string;
    apiProjectId : string;
    apiAuthenticationToken : string;
    dataProviderName : string;
    ciDataUpdateCronExpression : string;

    constructor(id : number = 0, projectTitle : string = null, apiHostUrl : string = null, apiProjectId : string = null, apiAuthenticationToken : string = null, dataProviderName : string = null, ciDataUpdateCronExpression : string = null) {
        this.id = id;
        this.projectTitle = projectTitle;
        this.apiHostUrl = apiHostUrl;
        this.apiProjectId = apiProjectId;
        this.apiAuthenticationToken = apiAuthenticationToken;
        this.dataProviderName = dataProviderName;
        this.ciDataUpdateCronExpression = ciDataUpdateCronExpression;
    }

    setCiDataUpdateCronExpression(intervalMinutes : number) {
        this.ciDataUpdateCronExpression = `*/${intervalMinutes} * * * *`;
    }

}
