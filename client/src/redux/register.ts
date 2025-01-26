import { createSlice } from "@reduxjs/toolkit";
import type { PayloadAction } from "@reduxjs/toolkit";

export interface IRegisterValidationErrors {
  Username: Array<string>;
  Password: Array<string>;
  ConfirmPassword: Array<string>;
}

export interface IRegisterError {
  message: string;
}

interface IInitialState {
  username: string;
  password: string;
  confirm_password: string;
  error: IRegisterError | null;
  validation_errors: IRegisterValidationErrors | null;
  is_submittable: boolean;
}

const initialState: IInitialState = {
  username: "",
  password: "",
  confirm_password: "",
  error: null,
  validation_errors: null,
  is_submittable: false,
};

interface ISetUsername {
  username: string;
}

interface ISetPassword {
  password: string;
}

interface ISetConfirmPassword {
  confirm_password: string;
}

interface ISetRegisterError {
  error: IRegisterError | null;
}

interface ISetValidationErrors {
  validation_errors: IRegisterValidationErrors | null;
}

interface ISetIsSubmittable {
  is_submittable: boolean;
}

export const register_slice = createSlice({
  name: "register",
  initialState,
  reducers: {
    setUsername(state: IInitialState, action: PayloadAction<ISetUsername>) {
      state.username = action.payload.username;
    },
    setPassword(state: IInitialState, action: PayloadAction<ISetPassword>) {
      state.password = action.payload.password;
    },
    setConfirmPassword(
      state: IInitialState,
      action: PayloadAction<ISetConfirmPassword>,
    ) {
      state.confirm_password = action.payload.confirm_password;
    },
    setError(state: IInitialState, action: PayloadAction<ISetRegisterError>) {
      state.error = action.payload.error;
    },
    setValidationErrors(
      state: IInitialState,
      action: PayloadAction<ISetValidationErrors>,
    ) {
      state.validation_errors = action.payload.validation_errors;
    },
    setIsSubmittable(
      state: IInitialState,
      action: PayloadAction<ISetIsSubmittable>,
    ) {
      state.is_submittable = action.payload.is_submittable;
    },
  },
});

export const {
  setUsername,
  setPassword,
  setConfirmPassword,
  setError,
  setValidationErrors,
  setIsSubmittable,
} = register_slice.actions;
export const register_reducer = register_slice.reducer;
