"use client";
import { useSelector } from "react-redux";
import { RootState, useAppDispatch } from "@/redux/store";
import styles from "@/styles/auth/welcome.module.scss";
import { logout } from "@/api/auth";

const Welcome = () => {
  const dispatch = useAppDispatch();
  const { self } = useSelector((state: RootState) => state.auth);
  const handleLogout = async () => {
    try {
      console.log("Hel");
      await dispatch(logout());
    } catch (e) {
      console.error(e);
    }
  };
  return (
    <div className={styles.container}>
      <span className={styles.welcome}>Welcome {self?.username}!</span>
      <button className={styles.logoutButton} onClick={handleLogout}>
        Logout
      </button>
    </div>
  );
};

export default Welcome;
