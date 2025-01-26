import { ISelf } from "@/api/auth";
import { createSlice } from "@reduxjs/toolkit";
import type { PayloadAction } from "@reduxjs/toolkit";

export enum AuthType {
  Login = 0,
  Register = 1,
}

interface IInitialState {
  auth_type: AuthType;
  self: ISelf | null;
}

const initialState: IInitialState = {
  auth_type: AuthType.Login,
  self: null,
};

interface ISetAuthType {
  auth_type: AuthType;
}

interface ISetSelf {
  self: ISelf | null;
}

export const auth_slice = createSlice({
  name: "auth",
  initialState,
  reducers: {
    setAuthType(state: IInitialState, action: PayloadAction<ISetAuthType>) {
      state.auth_type = action.payload.auth_type;
    },
    setSelf(state: IInitialState, action: PayloadAction<ISetSelf>) {
      state.self = action.payload.self;
    },
  },
});

export const { setAuthType, setSelf } = auth_slice.actions;
export const auth_reducer = auth_slice.reducer;
