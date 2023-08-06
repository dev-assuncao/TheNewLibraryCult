import { UserClaimsModel } from "./UserClaimsModel";

export class UserTokenModel {
    public Id: string;
    public Email: string;
    public Claims: UserClaimsModel[];
}
