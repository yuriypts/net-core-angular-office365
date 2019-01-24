
export class ReportUpdateValue {
    constructor(
        public id: number,
        public key: string,
        public name: string,
        public type: number,
        public value?: string
    ) {}
}
