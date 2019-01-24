import { LabelModel } from './label.model';

export class ReportRequestModel {
    constructor(
        public name: string,
        public reportTypeId: number,
        public values: LabelModel[]
    ) {}
}
