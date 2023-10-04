import { UserClaimsModel } from "./UserClaimsModel";

export class UserTokenModel {
    public id: string;
    public email: string;
    public claims: UserClaimsModel[];
}
