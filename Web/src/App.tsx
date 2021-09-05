import React from "react";
import { Logo } from "./components/Logo";
import { WhitelistAddForm } from "./components/WhitelistAddForm";
import { WhitelistList } from "./components/WhitelistList";
import { QueryClient, QueryClientProvider } from "react-query";
import { getSecretFromLocation } from "./api";
import { Card } from "./components/Card";

const queryClient = new QueryClient();

function App() {
    const hasSecret = getSecretFromLocation() !== "";

    return (
        <QueryClientProvider client={queryClient}>
            <div className="flex-row h-screen bg-blue-500">
                <div className="flex flex-col h-screen items-center justify-center gap-8">
                    <Logo />
                    {hasSecret && (
                        <>
                            <WhitelistAddForm />
                            <WhitelistList />
                        </>
                    )}
                    {!hasSecret && (
                        <Card error={true}>
                            <div className="text-lg p-2 text-center">
                                Nicht autorisiert!
                            </div>
                        </Card>
                    )}
                </div>
            </div>
        </QueryClientProvider>
    );
}

export default App;
