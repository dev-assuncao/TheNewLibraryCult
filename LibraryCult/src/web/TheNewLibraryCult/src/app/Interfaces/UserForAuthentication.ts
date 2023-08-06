interface UserForAuthentication {
    email: string;
    password: string;
}

export interface ILogin extends UserForAuthentication{
}

export interface IRegister extends UserForAuthentication{
    confirmPassword: string;
}