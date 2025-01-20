import React, { useRef } from "react";
import { useDispatch } from "react-redux";
import styles from "@/styles/auth/login.module.scss";
import { setPassword, setUsername } from "@/redux/login";
import { login } from "@/api/auth";
import { setUser } from "@/redux/auth";

const Login = () => {
  const dispatch = useDispatch();
  const username_ref = useRef<HTMLInputElement>(null);
  const password_ref = useRef<HTMLInputElement>(null);

  const handleUsernameInputChange = () => {
    dispatch(setUsername({ username: username_ref.current?.value ?? "" }));
  };

  const handlePasswordInputChange = () => {
    dispatch(setPassword({ password: password_ref.current?.value ?? "" }));
  };

  const handleSubmit = async (e: React.FormEvent<HTMLElement>) => {
    try {
      e.preventDefault();
      e.stopPropagation();
      if (!username_ref.current || !password_ref.current) return;
      const user = await login({
        username: username_ref.current.value,
        password: password_ref.current.value,
      });
      dispatch(
        setUser({
          user: { id: user.id, username: user.username, access_token: "" },
        }),
      );
    } catch (e) {
      console.error(e);
    }
  };

  return (
    <div className={styles.container}>
      <form className={styles.form} onSubmit={(e) => handleSubmit(e)}>
        <div className={styles.form_control}>
          <label>
            <b>Username</b>
          </label>
          <br />
          <input
            type="text"
            placeholder={"Username"}
            ref={username_ref}
            onChange={handleUsernameInputChange}
          />
        </div>
        <div className={styles.form_control}>
          <label>
            <b>Password</b>
          </label>
          <br />
          <input
            type="password"
            placeholder={"Password"}
            ref={password_ref}
            onChange={handlePasswordInputChange}
          />
        </div>
        <div className={styles.form_control}>
          <button className={styles.login_button} type="submit">
            Login
          </button>
        </div>
      </form>
    </div>
  );
};

export default Login;
