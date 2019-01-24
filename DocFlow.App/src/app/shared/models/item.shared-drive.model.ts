class OfficeUser {
    constructor(
        public displayName: string,
        public email: string,
        public id: string,
    ) {}
}

class Fields {
    constructor(
        public Created: string,
        public ContentType: string,
    ) {}
}

class CreatedBy {
    constructor(
        public user: OfficeUser
    ) {}
}

export class ItemSharedDriveModel {
    constructor(
        public webUrl: string,
        public fields: Fields,
        public createdDateTime: string,
        public createdBy: CreatedBy,
    ) {}
}
