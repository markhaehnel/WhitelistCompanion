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
            <div className="flex-row min-h-screen m-8">
                <div className="flex flex-col min-h-screen items-center justify-center gap-8">
                    <Logo />
                    {hasSecret && (
                        <>
                            <code className="text-2xl text-white font-mono">
                                mc.noob-box.net
                            </code>
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
