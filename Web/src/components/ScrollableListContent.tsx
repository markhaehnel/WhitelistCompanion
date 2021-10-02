import * as React from "react";
import { Box } from "@chakra-ui/layout";
import { Text, useColorModeValue } from "@chakra-ui/react";
import SimpleBar from "simplebar-react";

type ScrollableListContentProps = {
    items: string[];
    error?: string;
    empty: string;
    minMaxHeight: any;
};

const ScrollableListContent: React.FC<ScrollableListContentProps> = ({
    items,
    error,
    empty,
    minMaxHeight,
}) => {
    const itemBgOdd = useColorModeValue("gray.100", "gray.700");

    return (
        <Box
            as={SimpleBar}
            forceVisible="y"
            autoHide={false}
            clickOnTrack={false}
            minH={minMaxHeight}
            maxH={minMaxHeight}
        >
            {error && (
                <Text fontSize="md" px={4} py={2}>
                    {error}
                </Text>
            )}

            <Box transition="150ms linear" opacity={!error && items ? 100 : 0}>
                {items && items.length > 0 ? (
                    items.map((user: string) => (
                        <Box
                            fontSize="md"
                            _odd={{ background: itemBgOdd }}
                            px={4}
                            py={2}
                            key={user}
                        >
                            {user}
                        </Box>
                    ))
                ) : (
                    <Text fontSize="md" px={4} py={2}>
                        {empty}
                    </Text>
                )}
            </Box>
        </Box>
    );
};

export { ScrollableListContent };
