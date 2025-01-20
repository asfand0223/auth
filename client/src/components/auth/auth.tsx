import React from "react";
import { useSelector } from "react-redux";
import Register from "./register";
import Login from "./login";
import { AuthType } from "@/redux/auth";
import { RootState } from "@/redux/store";
import AuthControl from "./auth_control";
import styles from "@/styles/auth/auth.module.scss";

const Auth = () => {
  const { user, auth_type } = useSelector((state: RootState) => state.auth);
  return (
    <div className={styles.container}>
      <div className={styles.wrapper}>
        <AuthControl />
        {!user && (auth_type === AuthType.Register ? <Register /> : <Login />)}
      </div>
    </div>
  );
};

export default Auth;
