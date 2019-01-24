import { UserModel } from './user.model';
import { ReportValues } from './reportValues.model';
import { ReportTypeModel } from './reportType.model';

export class ReportModel {
    constructor(
        public driveType: number,
        public id: number,
        public isSigned: boolean,
        public reportTypeId: number,
        public name: string,
        public signerUser: UserModel,
        public values: ReportValues[],
        public reportType: ReportTypeModel
    ) {}
}
