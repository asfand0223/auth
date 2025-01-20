import React, { useRef } from "react";
import styles from "@/styles/auth/register.module.scss";
import { useDispatch } from "react-redux";
import { setUsername, setPassword, setConfirmPassword } from "@/redux/register";
import { register } from "@/api/auth";
import { setUser } from "@/redux/auth";

const Register = () => {
  const dispatch = useDispatch();
  const username_ref = useRef<HTMLInputElement>(null);
  const password_ref = useRef<HTMLInputElement>(null);
  const confirm_password_ref = useRef<HTMLInputElement>(null);

  const handleUsernameInputChange = () => {
    dispatch(setUsername({ username: username_ref.current?.value ?? "" }));
  };

  const handlePasswordInputChange = () => {
    dispatch(setPassword({ password: password_ref.current?.value ?? "" }));
  };

  const handleConfirmPasswordInputChange = () => {
    dispatch(
      setConfirmPassword({
        confirm_password: confirm_password_ref.current?.value ?? "",
      }),
    );
  };

  const handleFormSubmit = async (e: React.FormEvent<HTMLElement>) => {
    try {
      e.preventDefault();
      e.stopPropagation();
      if (
        !username_ref.current ||
        !password_ref.current ||
        !confirm_password_ref.current ||
        password_ref.current.value !== confirm_password_ref.current.value
      )
        return;
      const user = await register({
        username: username_ref.current.value,
        password: password_ref.current.value,
        confirm_password: confirm_password_ref.current.value,
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
      <form className={styles.form} onSubmit={(e) => handleFormSubmit(e)}>
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
          <label>
            <b>Confirm Password</b>
          </label>
          <br />
          <input
            type="password"
            placeholder={"Confirm Password"}
            ref={confirm_password_ref}
            onChange={handleConfirmPasswordInputChange}
          />
        </div>
        <div className={styles.form_control}>
          <button className={styles.register_button} type="submit">
            Register
          </button>
        </div>
      </form>
    </div>
  );
};

export default Register;
