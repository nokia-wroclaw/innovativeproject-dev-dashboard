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
    stageStatus : number;
}

export class SupportedProviders{
    data : string[];
    constructor(data : string[]) {}
}

export class Project {
    
    id : number;
    projectTitle : string;
    apiHostUrl : string;
    apiProjectId : string;
    apiAuthenticationToken : string;
    dataProviderName: string;
    ciDataUpdateCronExpression: string;

  constructor(projectTitle: string, apiHostUrl: string, apiProjectId: string, apiAuthenticationToken: string, dataProviderName: string, ciDataUpdateCronExpression: string) {}

  setCiDataUpdateCronExpression(intervalMinutes: number) {
    this.ciDataUpdateCronExpression = `*/${intervalMinutes} * * * *`;
  }
}
