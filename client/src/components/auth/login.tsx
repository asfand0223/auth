import React, { useEffect, useRef } from "react";
import { useDispatch } from "react-redux";
import styles from "@/styles/auth/login.module.scss";
import {
  setError,
  setIsSubmittable,
  setPassword,
  setUsername,
  setValidationErrors,
} from "@/redux/login";
import { login } from "@/api/auth";
import Error from "./error";
import { useSelector } from "react-redux";
import { RootState } from "@/redux/store";
import ValidationErrors from "./validation_errors";
import { authorise } from "@/api/auth";
import { setSelf } from "@/redux/auth";

const Login = () => {
  const dispatch = useDispatch();
  const { error, validation_errors, username, password, is_submittable } =
    useSelector((state: RootState) => state.login);
  const username_ref = useRef<HTMLInputElement>(null);
  const password_ref = useRef<HTMLInputElement>(null);

  useEffect(() => {
    dispatch(setError({ error: null }));
    dispatch(setValidationErrors({ validation_errors: null }));
  }, []);

  useEffect(() => {
    dispatch(
      setIsSubmittable({ is_submittable: username !== "" && password !== "" }),
    );
  }, [username, password]);

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
      dispatch(setError({ error: null }));
      dispatch(setValidationErrors({ validation_errors: null }));
      if (!username_ref.current || !password_ref.current) return;
      const response = await login({
        username: username_ref.current.value,
        password: password_ref.current.value,
      });
      if (response.status === 200) {
        const response = await authorise();
        dispatch(setSelf({ self: response }));
      } else if (response.error) {
        dispatch(setError({ error: { message: response.error } }));
      } else {
        dispatch(
          setValidationErrors({
            validation_errors: response.validation_errors,
          }),
        );
      }
    } catch (e) {
      console.error(e);
    }
  };

  return (
    <div className={styles.container}>
      {error && <Error error={error.message} />}
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
          <br />
          {validation_errors?.Username && (
            <ValidationErrors validation_errors={validation_errors.Username} />
          )}
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
          <br />

          {validation_errors?.Password && (
            <ValidationErrors validation_errors={validation_errors.Password} />
          )}
        </div>
        <div className={styles.form_control}>
          <button
            className={`${styles.login_button} ${is_submittable ? styles.login_button_active : ""}`}
            type="submit"
            disabled={!is_submittable}
          >
            Login
          </button>
        </div>
      </form>
    </div>
  );
};

export default Login;
