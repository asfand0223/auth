import { createSlice } from "@reduxjs/toolkit";
import type { PayloadAction } from "@reduxjs/toolkit";

export interface LoginValidationErrors {
  Username: Array<string>;
  Password: Array<string>;
}

interface IInitialState {
  username: string;
  password: string;
  error: string;
  validation_errors: LoginValidationErrors | null;
  is_submittable: boolean;
}

const initialState: IInitialState = {
  username: "",
  password: "",
  error: "",
  validation_errors: null,
  is_submittable: false,
};

interface ISetUsername {
  username: string;
}

interface ISetPassword {
  password: string;
}

interface ISetError {
  error: string;
}

interface ISetValidationErrors {
  validation_errors: LoginValidationErrors | null;
}

interface ISetIsSubmittable {
  is_submittable: boolean;
}

export const login_slice = createSlice({
  name: "login",
  initialState,
  reducers: {
    setUsername(state: IInitialState, action: PayloadAction<ISetUsername>) {
      state.username = action.payload.username;
    },
    setPassword(state: IInitialState, action: PayloadAction<ISetPassword>) {
      state.password = action.payload.password;
    },
    setError(state: IInitialState, action: PayloadAction<ISetError>) {
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
  setError,
  setValidationErrors,
  setIsSubmittable,
} = login_slice.actions;
export const login_reducer = login_slice.reducer;
