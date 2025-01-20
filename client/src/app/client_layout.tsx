"use client";
import React from "react";
import ReduxProvider from "@/providers/redux_provider";
import Navbar from "../components/navbar";

const ClientLayout = ({
  children,
}: Readonly<{ children: React.ReactNode }>) => {
  return (
    <ReduxProvider>
      <header>
        <Navbar />
      </header>
      <main>{children}</main>
    </ReduxProvider>
  );
};

export default ClientLayout;
