"use client";
import { useSelector } from "react-redux";
import { RootState } from "@/redux/store";
import Welcome from "@/components/auth/welcome";
import styles from "./page.module.scss";
import Auth from "@/components/auth/auth";

const Home = () => {
  const { access_token } = useSelector((state: RootState) => state.auth);
  return (
    <div className={styles.container}>
      {access_token && <Welcome />}
      {!access_token && <Auth />}
    </div>
  );
};

export default Home;
