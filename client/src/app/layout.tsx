import type { Metadata } from "next";
import ClientLayout from "./client_layout";
import "./globals.css";

export const metadata: Metadata = {
  title: "Auth",
  description: "JWT Auth App",
};

export default function RootLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return (
    <html lang="en">
      <body>
        <ClientLayout>{children}</ClientLayout>
      </body>
    </html>
  );
}
