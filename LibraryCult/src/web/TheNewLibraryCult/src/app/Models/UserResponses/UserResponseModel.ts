import { UserTokenModel } from "./UserTokenModel";

export class  UserResponseModel{
    public Status:boolean;
    public StatusCode:Number;
    public AccessToken:string;
    public ExpiresIn:Number;
    public UserToken:UserTokenModel;
    public Errors : string[];
}

