export class ReportLabelModel {
    constructor(
        public id: number,
        public name: string,
        public type: number
    ) {}
}

export enum LabelType {
    String,
    Int,
    Date,
    Bool
}
