import { LabelModel } from './label.model';

export class UpdateRequestModel {
    constructor(
        public name: string,
        public driveType: number,
        public reportTypeId: number,
        public reportId: number,
        public values: LabelModel[]
    ) {}
}
