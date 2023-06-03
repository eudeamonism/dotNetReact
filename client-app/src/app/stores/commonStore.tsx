import { makeAutoObservable } from "mobx";
import { ServerError } from "../models/serverError";

export default class CommonStore{
    //setting type
    error: ServerError | null = null;

    constructor() {
        makeAutoObservable(this);
    }

    setServerError(error: ServerError){
        this.error = error;
    }

}