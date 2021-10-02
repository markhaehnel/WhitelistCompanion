import React from "react";
import { Logo } from "./components/Logo";
import { WhitelistList } from "./components/WhitelistList";
import { QueryClient, QueryClientProvider } from "react-query";
import { InfoText } from "./components/InfoText";
import { WhitelistAddCard } from "./components/WhitelistAddCard";
import { AuthContainer } from "./components/AuthContainer";
import { PlayerList } from "./components/PlayerList";

const queryClient = new QueryClient();

function App() {
    return (
        <QueryClientProvider client={queryClient}>
            <Logo />
            <AuthContainer>
                <>
                    <InfoText />
                    <div className="flex flex-col xl:flex-row gap-4">
                        <div className="flex flex-col gap-4">
                            <WhitelistAddCard />
                            <WhitelistList />
                        </div>
                        <PlayerList />
                    </div>
                </>
            </AuthContainer>
        </QueryClientProvider>
    );
}

export default App;
