import { UserTokenModel } from "./UserTokenModel";

export class  UserResponseModel{
    public status:boolean;
    public statusCode:Number;
    public accessToken:string;
    public expiresIn:Number;
    public userToken:UserTokenModel;
    public errors : string[];
    public message:string;
}

