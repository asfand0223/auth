import React from "react";
import { AuthType, setAuthType } from "@/redux/auth";
import { useSelector } from "react-redux";
import { RootState, useAppDispatch } from "@/redux/store";
import styles from "@/styles/auth/auth_control.module.scss";

const AuthControl = () => {
  const dispatch = useAppDispatch();
  const { auth_type } = useSelector((state: RootState) => state.auth);

  const handleClick = (auth_type: AuthType) => {
    dispatch(setAuthType({ auth_type }));
  };

  return (
    <div className={styles.container}>
      <div
        className={`${styles.control} ${auth_type === AuthType.Register ? styles.control_inactive : ""}`}
        onClick={() => handleClick(AuthType.Login)}
      >
        LOGIN
      </div>
      <div
        className={`${styles.control} ${auth_type === AuthType.Login ? styles.control_inactive : ""}`}
        onClick={() => handleClick(AuthType.Register)}
      >
        REGISTER
      </div>
    </div>
  );
};

export default AuthControl;
