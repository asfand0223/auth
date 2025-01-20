import { createSlice } from "@reduxjs/toolkit";
import type { PayloadAction } from "@reduxjs/toolkit";

interface IInitialState {
  username: string;
  password: string;
  confirm_password: string;
}

const initialState: IInitialState = {
  username: "",
  password: "",
  confirm_password: "",
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
  },
});

export const { setUsername, setPassword, setConfirmPassword } =
  register_slice.actions;
export const register_reducer = register_slice.reducer;
