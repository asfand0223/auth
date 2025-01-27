"use client";
import styles from "./page.module.scss";
import Auth from "@/components/auth/auth";
import { useEffect } from "react";
import { authorise } from "@/api/auth";
import { setSelf } from "@/redux/auth";
import { useDispatch } from "react-redux";

const Home = () => {
  const dispatch = useDispatch();
  useEffect(() => {
    const getSelf = async () => {
      const response = await authorise();
      dispatch(setSelf({ self: response }));
    };
    getSelf();
  }, [dispatch]);
  return (
    <div className={styles.container}>
      <Auth />
    </div>
  );
};

export default Home;
