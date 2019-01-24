import { UserModel } from './user.model';

export class ReportHistoryModel {
    constructor(
        public createDate: string,
        public createUser: UserModel,
        public createUserId: string,
        public id: number,
        public reportId: number,
    ) {}
}
