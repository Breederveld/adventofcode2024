import System.IO
import Control.Monad
import Data.List

splitBy delimiter = foldr f [[]]
  where f c l@(x:xs) | c == delimiter = []:l | otherwise = (c:x):xs

prefixesOf :: String -> [String] -> [String]
prefixesOf word parts = filter (\x -> isPrefixOf x word) parts
isPossible :: String -> [String] -> Bool
isPossible [] _ = True
isPossible word parts = any (\x -> isPossible' word x parts) (prefixesOf word parts)

isPossible' :: String -> String -> [String] -> Bool
isPossible' word part parts = isPossible (drop (length part) word) parts

main = do
  handle <- openFile "input.txt" ReadMode
  contents <- hGetContents handle
  let contentLines = lines contents
  let towels = map (\s -> tail s) (splitBy ',' (" " ++ contentLines !! 0))
  let search = tail (tail contentLines)
  print (length (filter (\x -> isPossible x towels) search))
  hClose handle
