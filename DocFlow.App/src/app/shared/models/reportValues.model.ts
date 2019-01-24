import { ReportLabelModel } from './reportLabel.model';

export class ReportValues {
    constructor(
        public id: number,
        public reportId: number,
        public reportLabelId: number,
        public value: string,
        public reportLabel: ReportLabelModel
    ) {}
}
