import React from "react";
import { useSelector } from "react-redux";
import Register from "./register";
import Login from "./login";
import { AuthType } from "@/redux/auth";
import { RootState } from "@/redux/store";
import AuthControl from "./auth_control";
import styles from "@/styles/auth/auth.module.scss";
import Welcome from "./welcome";

const Auth = () => {
  const { self, auth_type } = useSelector((state: RootState) => state.auth);
  return (
    <div className={styles.container}>
      <div className={styles.wrapper}>
        {self && <Welcome />}
        {!self && <AuthControl />}
        {!self && (auth_type === AuthType.Register ? <Register /> : <Login />)}
      </div>
    </div>
  );
};

export default Auth;
