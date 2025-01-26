"use client";
import { useSelector } from "react-redux";
import { RootState } from "@/redux/store";
import Welcome from "@/components/auth/welcome";
import styles from "./page.module.scss";
import Auth from "@/components/auth/auth";
import { useEffect } from "react";
import { authorise } from "@/api/auth";
import { setSelf } from "@/redux/auth";
import { useDispatch } from "react-redux";

const Home = () => {
  const { self } = useSelector((state: RootState) => state.auth);
  const dispatch = useDispatch();
  useEffect(() => {
    const getSelf = async () => {
      const response = await authorise();
      dispatch(setSelf({ self: response }));
    };
    getSelf();
  }, []);
  return (
    <div className={styles.container}>
      {self && <Welcome />}
      {!self && <Auth />}
    </div>
  );
};

export default Home;
